export class CarQuery {
    public carTypeIds: number[];
    public carClassIds: number[];
    public carBrandIds: number[];
    public sortById: number;
    public searchTerm: string;

    public constructor(carTypeIds: number[], carClassIds: number[], carBrandIds: number[], sortById: number, searchTerm: string){
        this.carBrandIds = carBrandIds;
        this.carClassIds = carClassIds;
        this.carTypeIds = carTypeIds;
        this.searchTerm = searchTerm;
        this.sortById = sortById;
    }
}