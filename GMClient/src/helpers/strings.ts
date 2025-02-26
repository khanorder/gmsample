export class Strings {
    constructor() {
        this.data = new Map<number, string>();
        this.loadData();
    }

    private data: Map<number, string> = new Map<number, string>();

    private loadData(): void {
        this.data.set(1, "권한이 없거나 인증이 만료되었습니다.\n메인화면으로 이동하시겠습니까?");
    }

    public getString(key: number): string {
        if (this.data.has(key)) {
            return this.data.get(key) as string;
        } else {
            console.error(`not found string(${key}).`);
            return "";
        }
    }
}

const strings = new Strings();

export default strings;