import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, Validators, ɵInternalFormsSharedModule, ReactiveFormsModule } from '@angular/forms';
import { BookService } from '../../../core/services/book';
import { Auth } from '../../../core/services/auth';
import { ActivatedRoute, Router } from '@angular/router';
import { BookStatus } from '../../../core/models/book';

@Component({
  selector: 'app-book-form',
  imports: [ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './book-form.html',
  // styleUrl: './book-form.css',
})
export class BookForm implements OnInit {
  private fb = inject(FormBuilder);
  private bookService = inject(BookService);
  private auth = inject(Auth);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  editingId = signal<number | null>(null);
  editingUserId = signal<number | null>(null);
  isEditMode = signal(false);

  form = this.fb.nonNullable.group({
    title: ['', Validators.required],
    author: ['', Validators.required],
    status: ['wishlist' as BookStatus, Validators.required],
    rating: this.fb.control<number | null>(null)
  });

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (!idParam) {
      return;
    }

    const id = Number(idParam);
    this.editingId.set(id);
    this.isEditMode.set(true);

    this.bookService.getById(id).subscribe((book) => {
      this.editingUserId.set(book.userId);
      this.form.patchValue({
        title: book.title,
        author: book.author,
        status: book.status,
        rating: book.rating
      });
    });
  }

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const value = this.form.getRawValue();
    const curUserId = this.auth.curUser()?.id;

    if (!curUserId) {
      return;
    }

    if (this.isEditMode() && this.editingId() != null) {
      const ownerId = this.editingUserId();
      if (ownerId == null) {
        return;
      }
      this.bookService
        .update(this.editingId()!, { ...value, userId: ownerId })
        .subscribe(() => this.router.navigate(['/books']));
    } else {
      this.bookService
        .create({ ...value, userId: curUserId })
        .subscribe(() => this.router.navigate(['/books']));
    }
  }
}
