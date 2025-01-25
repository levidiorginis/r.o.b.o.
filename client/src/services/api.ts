import axios from 'axios'

export const urlBackend = 'http://localhost:5072';

const api = axios.create({
    baseURL: urlBackend,  // URL base configurada
    headers: {
        'Content-Type': 'application/json',    // Defina os cabeçalhos, se necessário
    },
});

export default api;