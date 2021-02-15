import { Photo } from "./Photo";

export interface member
{
    id: number;
    userName: string;
    photoUrl: string;
    age: number;
    knownAs: string;
    createdAt: Date;
    lastActive: Date;
    gender: string;
    introduction: string;
    lookingFor: string;
    interests: string;
    city: string;
    country: string;
    photo: Photo[];
}

