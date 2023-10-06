export interface AddAircraftSightRequest{
    make: string;
    model: string;
    registration: string;
    location: string;
    dateTime: string;
}

export interface GetAircraftSightRequest{
    make: string;
    model: string;
    registration: string;
    location: string;
    dateTime: Date;
    id: number,
    imagePath:string
}

export interface UpdateAircraftSightRequest{
    make: string;
    model: string;
    registration: string;
    location: string;
    dateTime: Date;
    id: number,
    imagePath ?:string
}