import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './BookReservation.css';

const BookReservation = () => {
    const [libros, setLibros] = useState([]);
    const [usuarios, setUsuarios] = useState([]);
    const [libroId, setLibroId] = useState('');
    const [usuarioId, setUsuarioId] = useState('');
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    useEffect(() => {
        const fetchData = async () => {
            try {
                const librosResponse = await axios.get('https://localhost:7066/api/Libro/Disponibles');
                const usuariosResponse = await axios.get('https://localhost:7066/api/Usuario');
                
                setLibros(librosResponse.data);
                setUsuarios(usuariosResponse.data);
            } catch (err) {
                console.error('Error fetching data:', err);
                setError('Error al cargar los datos. Intenta nuevamente más tarde.');
            }
        };

        fetchData();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');

        try {
            const response = await axios.post('https://localhost:7066/api/Prestamo/ReservarLibro', {
                LibroId: libroId,
                UsuarioId: usuarioId,
                FechaInicio: fechaInicio,
                FechaFin: fechaFin,
            });

            setSuccess('Préstamo registrado con éxito.');
        } catch (err) {
            let errorMessage = 'Error al registrar el préstamo.';
            if (err.response) {
                const errorData = err.response.data;
                errorMessage += ` Status: ${err.response.status}. Mensaje: ${typeof errorData === 'object' ? JSON.stringify(errorData) : errorData}`;
            } else if (err.request) {
                errorMessage += ' No se recibió respuesta del servidor.';
            } else {
                errorMessage += ` Error: ${err.message}`;
            }

            console.error('Error al registrar el préstamo:', err);
            setError(errorMessage);
        }
    };

    return (
        <div className="book-reservation-container">
            <h1>Reservar Libro</h1>
            <form onSubmit={handleSubmit}>
                <div className="form-group">
                    <label htmlFor="libro">Libro:</label>
                    <select
                        id="libro"
                        value={libroId}
                        onChange={(e) => setLibroId(e.target.value)}
                    >
                        <option value="">Seleccionar libro</option>
                        {libros.map((libro) => (
                            <option key={libro.id} value={libro.id}>
                                {libro.titulo}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="usuario">Usuario:</label>
                    <select
                        id="usuario"
                        value={usuarioId}
                        onChange={(e) => setUsuarioId(e.target.value)}
                    >
                        <option value="">Seleccionar usuario</option>
                        {usuarios.map((usuario) => (
                            <option key={usuario.id} value={usuario.id}>
                                {usuario.nombre}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="form-group">
                    <label htmlFor="fechaInicio">Fecha de Inicio:</label>
                    <input
                        type="date"
                        id="fechaInicio"
                        value={fechaInicio}
                        onChange={(e) => setFechaInicio(e.target.value)}
                        required
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="fechaFin">Fecha de Fin:</label>
                    <input
                        type="date"
                        id="fechaFin"
                        value={fechaFin}
                        onChange={(e) => setFechaFin(e.target.value)}
                        required
                    />
                </div>
                <button type="submit">Reservar</button>
            </form>
            {error && <p className="error-message">{error}</p>}
            {success && <p className="success-message">{success}</p>}
        </div>
    );
};

export default BookReservation;
