// src/components/Navbar.jsx
import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';


const Navbar = () => {
  return (
    <nav>
      <ul>
        <li><Link to="/">Lista de Libros</Link></li>
        <li><Link to="/buscar">Buscar Libro</Link></li>
        <li><Link to="/reservar">Reservar Libro</Link></li>
      </ul>
    </nav>
  );
};

export default Navbar;
