import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BookService } from '../../../core/services/book';
import { Book } from '../../../core/models/book';

@Component({
  selector: 'app-book-detail',
  imports: [RouterLink],
  templateUrl: './book-detail.html',
  // styleUrl: './book-detail.css',
})
export class BookDetail implements OnInit {
  private bookService = inject(BookService);
  private route = inject(ActivatedRoute);

  book = signal<Book | null>(null);

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.bookService.getById(id).subscribe((bookFromServer) => this.book.set(bookFromServer));
  }
}
