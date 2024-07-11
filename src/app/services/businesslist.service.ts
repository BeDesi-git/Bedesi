import {Injectable} from '@angular/core'
import { Business } from '../model/business-model';

@Injectable()
export class BusinessListService {

    businesses: Business[] = [] 
    constructor(){
      this.businesses.push()
    }
    getBusinessList(filterString: string){
      if (filterString == '')
        return BusinessList;
      else
        return BusinessList.filter(b=> b.name.toLowerCase().includes(filterString.toLowerCase()));
    }

    getBusiness(id: number){
        return BusinessList.find(business => business.id === id)
    }
}

const BusinessList = [
    {
      id: 1,
      name: 'Khalbatta',
      shortDescription: 'Home Caterier',
      description: 'Khalbatta Catering is a family-owned home catering service dedicated to making your gatherings unforgettable with our delicious, homemade meals and exceptional service.',
      imageUrl: '/assets/images/angularconnect-shield.png',
      contactNumber: 7878787878,
      website: 'www.khalbatta.com',
      instaLink: 'www.instagarm.com/khalbatta',
      services: ['HomeCaterier'],
      review: 5,
      reviewCount: 150,
      location: {
        address: 'HA1 1PA',
        city: 'London',
        country: 'England'
      },
      serviceArea: ['HA1']
    },
    {
      id: 2,
      name: 'Sam Dance Studio',
      shortDescription: 'Home Caterier',
      description: 'Khalbatta Catering is a family-owned home catering service dedicated to making your gatherings unforgettable with our delicious, homemade meals and exceptional service.',
      imageUrl: '/assets/images/ng-nl.png',
      contactNumber: 7878787878,
      website: 'www.khalbatta.com',
      instaLink: 'www.instagarm.com/khalbatta',
      services: ['DanceTutor'],
      review: 4.5,
      reviewCount: 100,
      location: {
        address: 'Harrow',
        city: 'London',
        country: 'UK'
      },
      serviceArea: ['HA1','HA2']
    },
    {
      id: 3,
      name: 'Global Bappa',
      shortDescription: 'Home Caterier',
      description: 'Khalbatta Catering is a family-owned home catering service dedicated to making your gatherings unforgettable with our delicious, homemade meals and exceptional service.',
      imageUrl: '/assets/images/ng-conf.png',
      contactNumber: 7878787878,
      website: 'www.khalbatta.com',
      instaLink: 'www.instagarm.com/khalbatta',
      services: ['FestiveProducts'],
      review: 4,
      reviewCount: 10,
      location: {
        address: 'Ryners Lane',
        city: 'London',
        country: 'United Kingdom'
      },
      serviceArea: ['HA1','HA2']
    },
    {
      id: 4,
      name: 'Bamboo House',
      shortDescription: 'Home Caterier',
      description: 'Khalbatta Catering is a family-owned home catering service dedicated to making your gatherings unforgettable with our delicious, homemade meals and exceptional service.',
      imageUrl: '/assets/images/basic-shield.png',
      contactNumber: 7878787878,
      website: 'www.khalbatta.com',
      instaLink: 'www.instagarm.com/khalbatta',
      services: ['IndianResturant'],
      review: 3.5,
      reviewCount: 250,
      location: {
        address: 'Ryners Lane',
        city: 'London',
        country: 'England'
      },
      serviceArea: ['HA1','HA3']
    },
    {
      id: 5,
      name: 'Prakash Tutions',
      shortDescription: 'Home Caterier',
      description: 'Khalbatta Catering is a family-owned home catering service dedicated to making your gatherings unforgettable with our delicious, homemade meals and exceptional service.',
      imageUrl: '/assets/images/ng-vegas.png',
      contactNumber: 7878787878,
      website: 'www.khalbatta.com',
      instaLink: 'www.instagarm.com/khalbatta',
      services: ['GCSETutor'],
      review: 4.3,
      reviewCount: 54,
      location: {
        address: 'Ryners Lane',
        city: 'London',
        country: 'England'
      },
      serviceArea: ['HA1','HA3','HA4']
    }
  ]