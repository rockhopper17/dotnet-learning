export type BookStatus = 'wishlist' | 'reading' | 'finished';

export interface Book {
    id: number;
    userId: number;
    title: string;
    author: string;
    status: BookStatus;
    rating: number | null;
}

// export type CreateBookRequest = Omit<Book, 'id' | 'userId'>;
// export type UpdateBookRequest = CreateBookRequest;
export type CreateBookRequest = Omit<Book, 'id'>;
export type UpdateBookRequest = CreateBookRequest;
// export type UpdateBookRequest = Omit<Book, 'id' | 'userId'>;