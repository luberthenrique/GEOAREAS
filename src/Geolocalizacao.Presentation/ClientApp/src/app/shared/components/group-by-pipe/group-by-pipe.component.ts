import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'groupBy'
})
export class GroupByPipe implements PipeTransform {
  transform(value: Array<any>, field: string): Array<any> {
    // prevents the application from breaking if the array of objects doesn't exist yet
    if (!value) {
      return null;
    }
    const groupedObj = value.reduce((previousVal, currentVal) => {
      if (!previousVal[currentVal[field]]) {
        previousVal[currentVal[field]] = [currentVal];
      } else {
        previousVal[currentVal[field]].push(currentVal);
      }
      return previousVal;
    }, {});
    // this will return an array of objects, each object containing a group of objects
    return Object.keys(groupedObj).map(key => ({
      key,
      value: groupedObj[key]
    }));
  }

}
