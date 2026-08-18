import { Service, computed, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

import { environment } from '../../../environments/environment';
import { AuthResponse, User } from '../models/user';

const TOKEN_KEY = 'book_tracker_token';
const USER_KEY = 'book_tracker_user';

function decodeUserFromToken(token: string): User {
    const payload = JSON.parse(atob(token.split('.')[1]));  // split JWT which has 3 parts separate by .
    return { id: payload.sub, email: payload.email ?? ''};
}

@Service()
export class Auth {
    private http = inject(HttpClient);
    private router = inject(Router);

    private curUserSignal = signal<User | null>(this.readStoredUser());

    curUser = this.curUserSignal.asReadonly();
    isAuthenticated = computed(() => this.curUserSignal() != null);

    register(email: string, password: string): Observable<AuthResponse> {
        return this.http
            .post<AuthResponse>(`${environment.apiUrl}/register`, { email, password })
            .pipe(tap((response) => this.storeSession(response)));
    }

    login(email: string, password: string): Observable<AuthResponse> {
        return this.http
            .post<AuthResponse>(`${environment.apiUrl}/login`, { email, password })
            .pipe(tap((response) => this.storeSession(response)));
    }

    logout(): void {
        localStorage.removeItem(TOKEN_KEY);
        localStorage.removeItem(USER_KEY);
        this.curUserSignal.set(null);
        this.router.navigate(['/login']);
    }

    getToken(): string | null {
        return localStorage.getItem(TOKEN_KEY);
    }
    
    private storeSession(response: AuthResponse): void {
        const user = decodeUserFromToken(response.accessToken);
        localStorage.setItem(TOKEN_KEY, response.accessToken);
        localStorage.setItem(USER_KEY, JSON.stringify(user));
        this.curUserSignal.set(user);
        // localStorage.setItem(USER_KEY, JSON.stringify(response.user));
        // this.curUserSignal.set(response.user);
    }

    private readStoredUser(): User | null {
        const raw = localStorage.getItem(USER_KEY);
        return raw ? (JSON.parse(raw) as User) : null;
    }
}
