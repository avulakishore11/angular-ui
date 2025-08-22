import { Component, OnInit } from '@angular/core';
import { Item } from './item';
import { ItemService } from './item.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [ItemService]
})
export class AppComponent implements OnInit {
  // List of items retrieved from the API
  items: Item[] = [];
  // Name for a new item entered by the user
  newItemName = '';

  constructor(private itemService: ItemService) {}

  /**
   * Lifecycle hook that is called after data-bound properties are
   * initialized. Loads existing items from the API.
   */
  ngOnInit(): void {
    this.loadItems();
  }

  /**
   * Retrieves the list of items from the backend API.
   */
  loadItems(): void {
    this.itemService.getItems().subscribe((data) => {
      this.items = data;
    });
  }

  /**
   * Adds a new item by sending it to the API, then refreshes the list.
   */
  addItem(): void {
    const name = this.newItemName.trim();
    if (!name) {
      return;
    }
    const item: Item = { name };
    this.itemService.addItem(item).subscribe((created) => {
      this.items.push(created);
      this.newItemName = '';
    });
  }

  /**
   * Deletes an item by its identifier.
   * @param id Identifier of the item to delete
   */
  deleteItem(id: number): void {
    this.itemService.deleteItem(id).subscribe(() => {
      this.items = this.items.filter((i) => i.id !== id);
    });
  }
}