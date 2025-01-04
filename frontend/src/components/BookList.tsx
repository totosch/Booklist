import React, { useState, useEffect } from 'react';
import api from '../services/api';
import './BookList.css';

interface Book {
  id: number;
  titulo: string;
  autor: string;
}

const BookList: React.FC = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [newBook, setNewBook] = useState({ titulo: '', autor: '' });
  const [filter, setFilter] = useState('');
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchBooks = async () => {
      try {
        const response = await api.get<Book[]>('/api/libro');
        setBooks(response.data);
      } catch (err: any) {
        setError(err.message);
      }
    };
    fetchBooks();
  }, []);

  const addBook = async () => {
    if (!newBook.titulo || !newBook.autor) return;
    try {
      const response = await api.post('/api/libro', newBook);
      setBooks([...books, response.data]);
      setNewBook({ titulo: '', autor: '' });
    } catch (err: any) {
      setError(err.message);
    }
  };

  const deleteBook = async (id: number) => {
    try {
      await api.delete(`/api/libro/${id}`);
      setBooks(books.filter((book) => book.id !== id));
    } catch (err: any) {
      setError(err.message);
    }
  };

  const filteredBooks = books.filter(
    (book) =>
      book.titulo.toLowerCase().includes(filter.toLowerCase()) ||
      book.autor.toLowerCase().includes(filter.toLowerCase())
  );

  return (
    <div className="book-list-container">
      <h1 className="title">Lista de Libros</h1>

      <input
        type="text"
        placeholder="Título del libro"
        value={newBook.titulo}
        onChange={(e) => setNewBook({ ...newBook, titulo: e.target.value })}
        className="input"
      />
      <input
        type="text"
        placeholder="Autor del libro"
        value={newBook.autor}
        onChange={(e) => setNewBook({ ...newBook, autor: e.target.value })}
        className="input"
      />
      <button onClick={addBook} className="button">
        Agregar Libro
      </button>

      <input
        type="text"
        placeholder="Buscar por título o autor"
        value={filter}
        onChange={(e) => setFilter(e.target.value)}
        className="input"
      />

      <ul className="book-list">
        {filteredBooks.map((book) => (
          <li key={book.id} className="book-item">
            <span className="book-title">{book.titulo}</span>
            <span className="book-author">{book.autor}</span>
            <button className="delete-button" onClick={() => deleteBook(book.id)}>
              x
            </button>
          </li>
        ))}
      </ul>

      {error && <p className="error-message">Error: {error}</p>}
    </div>
  );
};

export default BookList;
