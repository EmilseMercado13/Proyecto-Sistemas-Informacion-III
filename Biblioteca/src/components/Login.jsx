import React, { useState } from 'react';
import axios from 'axios';
import { useAuth } from '../context/AuthContext';
import './Login.css'; // Importar el archivo CSS

const Login = () => {
    const [correo, setCorreo] = useState('');
    const [nombre, setNombre] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    const { login } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        try {
            const response = await axios.post('https://localhost:7066/api/Usuario/Login', {
                CorreoElectronico: correo,
                Nombre: nombre
            });

            if (response.status === 200) {
                login(); // Set the user as logged in
                setSuccess('Inicio de sesión exitoso.');
                // Redirect or handle post-login actions
            }
        } catch (err) {
            let errorMessage = 'Error al iniciar sesión.';
            if (err.response) {
                const errorData = err.response.data;
                errorMessage += ` Status: ${err.response.status}. Mensaje: ${typeof errorData === 'object' ? JSON.stringify(errorData) : errorData}`;
            } else if (err.request) {
                errorMessage += ' No se recibió respuesta del servidor.';
            } else {
                errorMessage += ` Error: ${err.message}`;
            }

            setError(errorMessage);
        }
    };

    return (
        <div className="login-container">
            <h1>Iniciar Sesión</h1>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="correo">Correo Electrónico:</label>
                    <input
                        type="email"
                        id="correo"
                        value={correo}
                        onChange={(e) => setCorreo(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="nombre">Nombre:</label>
                    <input
                        type="text"
                        id="nombre"
                        value={nombre}
                        onChange={(e) => setNombre(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Iniciar Sesión</button>
            </form>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            {success && <p style={{ color: 'green' }}>{success}</p>}
        </div>
    );
};

export default Login;
