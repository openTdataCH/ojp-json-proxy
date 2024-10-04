import {createI18n, type DefineDateTimeFormat} from "vue-i18n";

const msgDeCh = await import('@/locales/messages/de/de-CH.json');

const msgEnCh = await import('@/locales/messages/en/en-CH.json');

const dtDeCH = await import('@/locales/date-time-formats/de-CH.json');

type MessageSchema = typeof msgEnCh;
type DateTimeSchema = typeof dtDeCH;
export type SupportedLocale = ('en-CH' | 'de-CH');

const i18n = createI18n<[MessageSchema, DateTimeSchema], SupportedLocale, false>({
    legacy: false,
    allowComposition: true,
    locale: 'en-CH',
    fallbackLocale: 'en-CH',
    messages: {
        'en-CH': msgEnCh,
        'de-CH': msgDeCh
    },
    datetimeFormats: {
        'en-CH': dtDeCH as DefineDateTimeFormat,
        'de-CH': dtDeCH as DefineDateTimeFormat,
    },
    modifiers: {
        lcfirst: str => `${str.toString().charAt(0).toLowerCase()}${str.toString().substring(1)}`,
        ucfirst: str => `${str.toString().charAt(0).toUpperCase()}${str.toString().substring(1)}`,
    },
});

export default i18n;