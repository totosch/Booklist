import axios from 'axios';

const api = axios.create({
  baseURL: 'https://localhost:7241', // Cambia esto si es necesario
});

export default api;
