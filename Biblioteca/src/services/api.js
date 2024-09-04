// src/services/api.js
import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7066/api', // Cambia esto a tu URL base de la API
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
