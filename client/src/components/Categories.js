import React from 'react'
import CategoryCard from './CategoryCard';
import { FaPaintBrush, 
         FaCamera, 
         FaMusic, 
         FaFootballBall, 
         FaLaptopCode, 
         FaGamepad, 
         FaLayerGroup,
         FaBook} from 'react-icons/fa';
import { Button } from '@nextui-org/react';


function Categories() {
  return (
    <div className='flex flex-col items-center mt-20'>
        <div className=''>
            <h1 className='my-6 text-4xl font-bold'>Browse Categories</h1>
        </div>
        <div className='grid grid-cols-2 gap-8 mb-10 sm:grid-cols-4'>
            <CategoryCard
                ImageName='/artImage.png'
                IconName={FaPaintBrush}
                CategoryName='Art'
            />
            <CategoryCard
                ImageName='/photographyImage.png'
                IconName={FaCamera}
                CategoryName='Photography'
            />
            <CategoryCard
                ImageName='/musicImage.png'
                IconName={FaMusic}
                CategoryName='Music'
            />
            <CategoryCard
                ImageName='/sportImage.png'
                IconName={FaFootballBall}
                CategoryName='Sport'
            />
            <CategoryCard
                ImageName='/virtualWorldImage.png'
                IconName={FaLaptopCode}
                CategoryName='Art'
            />
            <CategoryCard
                ImageName='/gamingImage.png'
                IconName={FaGamepad}
                CategoryName='Gaming'
            />
            <CategoryCard
                ImageName='/collectionImage.png'
                IconName={FaLayerGroup}
                CategoryName='Collection'
            />
            <CategoryCard
                ImageName='/utilityImage.png'
                IconName={FaBook}
                CategoryName='Utility'
            />
        </div>
        <div>
        <Button
            radius="full"
            size="md"
            color="primary"
            variant="bordered"
            className="px-10 text-white"
          >
            View More
          </Button>
        </div>
    </div>
  )
}

export default Categories;