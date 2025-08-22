import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Item } from './item';

@Injectable()
export class ItemService {
  // Base URL of the backend API. In production this should point to the
  // Kubernetes service exposing the API. When running locally you can
  // override this to `http://localhost:5000/api/items`.
  private apiUrl = 'http://api-service:5000/api/items';

  constructor(private http: HttpClient) {}

  /**
   * Retrieve all items from the API.
   */
  getItems(): Observable<Item[]> {
    return this.http.get<Item[]>(this.apiUrl);
  }

  /**
   * Create a new item by issuing a POST request.
   * @param item The item to add
   */
  addItem(item: Item): Observable<Item> {
    return this.http.post<Item>(this.apiUrl, item);
  }

  /**
   * Delete an item by id.
   * @param id The identifier of the item to delete
   */
  deleteItem(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}