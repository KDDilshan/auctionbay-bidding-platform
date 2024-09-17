import React from 'react'
import Image from 'next/image';


function CategoryCard({ImageName,IconName, CategoryName}) {
  return (
    <div className="relative w-[200px] h-[250px] rounded-lg overflow-hidden ">
      <div className="absolute inset-0">
        <Image
          src={ImageName}
          alt="Category Background"
          layout="fill"
          objectFit="cover"
          className="blur-[1px] "
        />
      </div>
      <div className="relative flex flex-col items-center justify-center h-full ">
        <IconName className="mt-10 text-center text-white text-7xl" />
        <h2 className="w-full p-4 mt-auto text-2xl font-light text-center text-white bg-gray-900">{CategoryName}</h2>
      </div>
    </div>
  )
}

export default CategoryCard;