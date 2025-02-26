export class PaginatedList<T> {
    public totalItems: T[] = [];
    public page: number = 1;
    public pageSize: number = 10;
    public pageBlockSize: number = 10;
    public totalPages: number = 1;
    public startPage: number = 1;
    public endPage: number = 1;
    public offsetIndex: number = 0;
    public parameters: string = "";
    public items: T[] = [];

    constructor(items: T[], page?: number, parameters?: string, pageSize?: number, pageBlockSize?: number) {
        this.init(items, page, parameters, pageSize, pageBlockSize);
    }

    public init(items?: T[], page?: number, parameters?: string, pageSize?: number, pageBlockSize?: number) {
        this.totalItems = items ?? [];
        this.page = !page || 1 > page ? 1 : page;
        this.pageSize = !pageSize || 1 > pageSize ? 10 : pageSize;
        this.pageBlockSize = !pageBlockSize || 1 > pageBlockSize ? 10 : pageBlockSize;
        this.totalPages = 0 < this.total ? Math.ceil(this.total / this.pageSize) : 1;
        this.startPage = (Math.floor((this.page - 1) / this.pageBlockSize) * this.pageBlockSize) + 1;
        this.endPage = (this.startPage + this.pageBlockSize) <= this.totalPages ? (this.startPage + this.pageBlockSize - 1) : this.totalPages;
        var skip = (this.pageSize * (this.page - 1));
        this.offsetIndex = 1 > this.total ? 0 : this.total - skip;
        this.parameters = parameters ?? "";
        this.items = items ? this.totalItems.slice(skip, skip + this.pageSize) : [];
    }

    public setPage(page: number, parameters: string) {
        if (1 > this.totalItems.length) {
            this.init([]);
            return;
        }

        this.init(this.totalItems, page, parameters);
    }

    get length(): number {
        return this.items.length;
    }

    get total(): number {
        return this.totalItems ? this.totalItems.length : 0;
    }

    get hasPrevious(): boolean {
        return this.page > 1;
    }

    get hasPreviousPageBlock(): boolean {
        return (this.startPage - this.pageBlockSize) > 0;
    }

    get hasNext(): boolean {
        return this.page < this.totalPages;
    }

    get hasNextPageBlock(): boolean {
        return this.endPage < this.totalPages;
    }
}