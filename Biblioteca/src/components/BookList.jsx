import React, { useState, useEffect } from 'react';
import api from '../services/api';
import './BookList.css'; // Importar el archivo CSS

const BookList = () => {
  const [books, setBooks] = useState([]);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const response = await api.get('/Libro/Disponibles');
        setBooks(response.data);
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    };
    fetchBooks();
  }, []);

  return (
    <div className="book-list-container">
      <h1>Lista de Libros Disponibles</h1>
      <table className="book-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Título</th>
            <th>Autor</th>
            <th>Género</th>
            <th>Publicado</th>
          </tr>
        </thead>
        <tbody>
          {books.map(book => (
            <tr key={book.id}>
              <td>{book.id}</td>
              <td>{book.titulo}</td>
              <td>{book.autor}</td>
              <td>{book.genero}</td>
              <td>{book.anoPublicacion}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default BookList;
