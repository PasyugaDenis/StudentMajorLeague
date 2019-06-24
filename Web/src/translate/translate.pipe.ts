import { Pipe, PipeTransform } from '@angular/core';

import { LANG_EN_NAME, LANG_EN_TRANS } from './en';
import { LANG_RU_NAME, LANG_RU_TRANS } from './ru';
import { LANG_UK_NAME, LANG_UK_TRANS } from './uk';

@Pipe({
    name: 'translate'
})
export class TranslatePipe implements PipeTransform {
    currentLang: string = navigator.language.split('-')[0];

    dictionary = {
        [LANG_EN_NAME]: LANG_EN_TRANS,
        [LANG_RU_NAME]: LANG_RU_TRANS,
        [LANG_UK_NAME]: LANG_UK_TRANS
    };

    transform(value: string, args?: any): string {
        //alert(this.currentLang);
        if (!value) return;
            
        if (this.dictionary[this.currentLang] && this.dictionary[this.currentLang][value]) {
			return this.dictionary[this.currentLang][value];
		}

        return value;
    }
}