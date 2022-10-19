import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EnumService {

  constructor() { }

  enumSelector(definition) {
    return Object.keys(definition).filter(f => isNaN(Number(f)))
      .map(key => ({ value: definition[key], title: key }));
  }
}
