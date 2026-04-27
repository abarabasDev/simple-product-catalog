import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {
  title = signal('Strona główna');

  updateTitle(newTitle: string) {
    this.title.set(newTitle);
  }
}