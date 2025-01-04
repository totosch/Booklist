import React from 'react';
import BookList from './components/BookList';
import './App.css';

const App = () => {
  return (
    <div className="container">
      <div className="book-list-card">
        <BookList />
      </div>
    </div>
  );
};

export default App;
