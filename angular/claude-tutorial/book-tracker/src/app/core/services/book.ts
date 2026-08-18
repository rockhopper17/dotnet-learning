import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { Book, CreateBookRequest, UpdateBookRequest } from '../models/book';

@Service()
export class BookService {
    private http = inject(HttpClient);
    private baseUrl = `${environment.apiUrl}/books`;

    getAll(): Observable<Book[]> {
        return this.http.get<Book[]>(this.baseUrl);
    }

    getById(id: number): Observable<Book> {
        return this.http.get<Book>(`${this.baseUrl}/${id}`);
    }

    create(request: CreateBookRequest): Observable<Book> {
        return this.http.post<Book>(this.baseUrl, request);
    }

    update(id: number, request: UpdateBookRequest): Observable<Book> {
        return this.http.put<Book>(`${this.baseUrl}/${id}`, request);
    }

    delete(id: number): Observable<void> {
        return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }
}
