import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {
  transform(value: string, maxLength: number): string {
    if (value.length > maxLength) {
        let truncatedString = value.substring(0, maxLength);

        if (truncatedString.charAt(truncatedString.length - 1) === '.' && !truncatedString.endsWith('..')) {
          truncatedString = truncatedString.substring(0, truncatedString.length - 1) + '..';
        } else {
          truncatedString += '...';
        }
        
        return truncatedString;
    } else {
      return value;
    }
  }
}