
export class Business {
    businessId: number = 0;
    name: string = '';
    address: string = '';
    postcode: string = '';
    description?: string;
    contactNumber: string = '';
    email?: string = '';
    website?: string = '';
    imageUrl?: string = '';
    instaHandle?: string = '';
    facebook?: string = '';
    hasLogo?: boolean = false;
    servesPostcodes: string[] = [];
    keywords: string[] = [];
    isActive: boolean = false;
    ratings?: number = 0;
    agreeToShow: boolean = false;
    isOnline: boolean = false;
};


