import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'listFilter' })
export class ListFilterPipe implements PipeTransform {
  transform(list: any[], filterText: string): any {
    return list
      ? list.filter((item) => {
          return (
            item.make.search(new RegExp(filterText, 'i')) > -1 ||
            item.model.search(new RegExp(filterText, 'i')) > -1 ||
            item.registration.search(new RegExp(filterText, 'i')) > -1
          );
        })
      : [];
  }
}
