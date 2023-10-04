export interface AddAircraftSightRequest{
    make: string;
    model: string;
    registration: string;
    location: string;
    dateTime: Date;
}

export interface GetAircraftSightRequest{
    make: string;
    model: string;
    registration: string;
    location: string;
    dateTime: Date;
    id: number
}