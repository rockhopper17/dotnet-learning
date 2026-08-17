export interface User {
    id: number;
    email: string;
}

export interface AuthResponse {
    accessToken: string;
    user: User;
}