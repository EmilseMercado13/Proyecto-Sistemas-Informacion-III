import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Navbar from './components/Navbar';
import BookList from './components/BookList';
import BookSearch from './components/BookSearch';
import BookReservation from './components/BookReservation';
import Login from './components/Login';
import PrivateRoute from './components/PrivateRoute';
import { AuthProvider } from './context/AuthContext';
import './App.css';

const App = () => {
  return (
    <AuthProvider>
      <Router>
        <Navbar />
        <main>
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/" element={<BookList />} />
            <Route path="/buscar" element={<BookSearch />} />
            <Route path="/reservar" element={<PrivateRoute element={<BookReservation />} />} />
          </Routes>
        </main>
      </Router>
    </AuthProvider>
  );
};

export default App;
