import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BookService } from '../../../core/services/book';
import { Auth } from '../../../core/services/auth';
import { Book } from '../../../core/models/book';

@Component({
  selector: 'app-book-list',
  imports: [RouterLink],
  templateUrl: './book-list.html',
  // styleUrl: './book-list.css',
})
export class BookList implements OnInit {
  private bookService = inject(BookService);
  auth = inject(Auth);

  books = signal<Book[]>([]);

  ngOnInit(): void {
    this.load();
  }

  load() : void {
    this.bookService.getAll().subscribe((books) => this.books.set(books));
  }

  onDelete(id: number): void {
    if (!confirm('delete this book?')) {
      return;
    }
    this.bookService.delete(id).subscribe(() => this.load());
  }
}
