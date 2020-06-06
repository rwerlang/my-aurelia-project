export class NumberValueConverter {
    fromView(value: string): number {
        return parseInt(value) || 0;
    }
}