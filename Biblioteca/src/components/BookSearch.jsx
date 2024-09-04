import React, { useState } from 'react';
import api from '../services/api';
import './BookSearch.css';

const BookSearch = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [searchBy, setSearchBy] = useState('titulo'); // 'titulo' o 'autor'
  const [result, setResult] = useState([]);

  const handleSearch = async (e) => {
    e.preventDefault();
    try {
      // Modificar la URL de búsqueda según el criterio
      const response = await api.get(`/Libro/BuscarDisponible?${searchBy}=${searchTerm}`);
      setResult(response.data);
    } catch (error) {
      console.error('Error searching for book:', error);
      setResult([]);
    }
  };

  return (
    <div className="book-search-container">
      <h1>Buscar Libro</h1>
      <form onSubmit={handleSearch} className="book-search-form">
        <div className="form-group">
          <label htmlFor="searchBy">Buscar por:</label>
          <select
            id="searchBy"
            value={searchBy}
            onChange={(e) => setSearchBy(e.target.value)}
            className="form-select"
          >
            <option value="titulo">Título</option>
            <option value="autor">Autor</option>
          </select>
        </div>
        <div className="form-group">
          <input
            type="text"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            placeholder={`Introduce el ${searchBy}`}
            className="form-input"
          />
        </div>
        <button type="submit" className="form-button">Buscar</button>
      </form>
      {result.length > 0 ? (
        <table className="book-search-results">
          <thead>
            <tr>
              <th>Título</th>
              <th>Autor</th>
              <th>ISBN</th>
              <th>Año de Publicación</th>
              <th>Género</th>
              <th>Número de Páginas</th>
              <th>Idioma</th>
              <th>Editorial</th>
            </tr>
          </thead>
          <tbody>
            {result.map((book) => (
              <tr key={book.id}>
                <td>{book.titulo}</td>
                <td>{book.autor}</td>
                <td>{book.isbn}</td>
                <td>{book.anoPublicacion}</td>
                <td>{book.genero}</td>
                <td>{book.numeroPaginas}</td>
                <td>{book.idioma}</td>
                <td>{book.editorial}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        searchTerm && <p>El libro no está disponible o no existe.</p>
      )}
    </div>
  );
};

export default BookSearch;
