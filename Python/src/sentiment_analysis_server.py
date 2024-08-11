from flask import Flask, request

import json

import numpy as np
import torch

from transformers import AutoTokenizer, AutoModelForSequenceClassification, AutoConfig
from scipy.special import softmax

app = Flask(__name__)

# Model taken from https://huggingface.co/cardiffnlp/twitter-roberta-base-sentiment-latest
torch.device('cuda' if torch.cuda.is_available() else 'cpu')

model_path = 'cardiffnlp/twitter-roberta-base-sentiment-latest'
model = AutoModelForSequenceClassification.from_pretrained(model_path)
tokenizer = AutoTokenizer.from_pretrained(model_path)
config = AutoConfig.from_pretrained(model_path)

labels = ['Negative', 'Neutral', 'Positive']

@app.after_request
def add_headers(response):
    response.headers['Access-Control-Allow-Origin'] = "*"
    response.headers['Access-Control-Allow-Headers'] = "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With"
    response.headers['Access-Control-Allow-Methods'] = "POST"

    return response


@app.route("/sentiment", methods=["POST"])
def sentiment_analysis():
    encoded_input = tokenizer(request.json['content'], return_tensors='pt')
    output = model(**encoded_input)
    scores = softmax(output[0][0].detach().numpy())

    ranking = np.argsort(scores)

    return json.dumps({'sentiment': config.id2label[ranking[-1]], 'confidence': str(round(scores[ranking[-1]], 2))})


if __name__ == "__main__":
    app.run(debug=True, host='0.0.0.0', port=5000)