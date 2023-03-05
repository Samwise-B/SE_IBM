'use strict';
const express = require('express');
const path = require('path');
const app = express();
app.use(express.static(path.join(__dirname, 'Static')));
app.use(express.urlencoded());
app.use(express.json());

module.exports = app;
