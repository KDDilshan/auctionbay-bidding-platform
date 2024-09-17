import React from 'react'
import { useState } from 'react';
import Image from 'next/image';
import { Button } from '@nextui-org/react';

function CreatorCard({backGroundImage, creatorName, creatorProfileImage, currentFollowers}) {
    const [followers, setFollowers] = useState(currentFollowers);
    const [isFollowing, setIsFollowing] = useState(false);
    
    // Function to handle follow button click
    const handleFollow = () => {
        if (isFollowing) {
            setFollowers(followers - 1);
        } else {
            setFollowers(followers + 1);
        }
        setIsFollowing(!isFollowing);
    
    }


  return (
    <div className="relative w-full h-full max-w-xs mx-auto bg-center bg-cover rounded-lg shadow-lg" 
        style={{ backgroundImage: `url(${backGroundImage})` }} >
      <div className="relative flex justify-center mt-12">
        <Image
          src={creatorProfileImage}
          alt="Profile Image"
          className="mt-20 -mb-10 border-4 border-white rounded-full shadow-lg"
          width={70}
          height={70}
        />
      </div>

      
      <div className="p-6 mt-4 text-center text-white bg-gray-800 rounded-lg ">
        <h3 className="text-xl font-semibold ">{creatorName}</h3>
        <p className="mt-2 font-semibold text-yellow-500">Followers: {followers}</p>
        
        <Button
          radius="full"
          className={`${isFollowing ? 'bg-transparent border-2 border-gradient-to-tr from-blue-300 to-blue-950 text-white mt-4 px-10' : 
                     'text-white shadow-lg bg-gradient-to-tr from-blue-300 to-blue-700 px-14 mt-4'} hover:opacity-80 transition px-10`}
                     onClick={handleFollow}
        >
          {isFollowing ? 'Following' : 'Follow'}
        </Button>
      </div>
    </div>
  )
}

export default CreatorCard