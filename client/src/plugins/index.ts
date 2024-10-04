import type {App} from "vue";
import i18n from "@/plugins/i18n";

export function registerPlugins(app: App) {
    app.use(i18n);
}