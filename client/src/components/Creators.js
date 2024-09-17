import React from 'react'
import CreatorCard from './CreatorCard';
import { Button } from '@nextui-org/react';

function Creators() {
    const users = [
        {
          backGroundImage:'/user1Background.jpeg' , 
          creatorProfileImage:'/user1.jpeg', 
          creatorName: 'John Doe',
          currentFollowers: 10,
        },
        {
            backGroundImage:'/user2Background.jpeg' , 
            creatorProfileImage:'/user1.jpeg', 
            creatorName: 'John Doe',
            currentFollowers: 120,
        },
        {
            backGroundImage:'user3Background.jpeg' , 
            creatorProfileImage:'/user1.jpeg', 
            creatorName: 'John Doe',
            currentFollowers: 50,
        },
        {
            backGroundImage:'user4Background.jpeg' , 
            creatorProfileImage:'/user1.jpeg', 
            creatorName: 'John Doe',
            currentFollowers: 20,
        },
      ];
    
  return (
    <div className="container p-4 mx-auto ">
        <h1 className="mb-10 text-4xl font-semibold text-center">Top List Creators</h1>
        <div className="flex justify-center my-8 space-x-4">
        <Button 
            color="primary" 
            variant="ghost"
            radius="full"
            className='text-white'>
          Popular
        </Button>
        
        <Button 
            color="primary" 
            variant="ghost"
            radius="full"
            className='text-white'>
          Following
        </Button>
      </div>
      <div className="grid grid-cols-1 gap-8 sm:grid-cols-2 md:grid-cols-2 lg:grid-cols-4 ">
        {users.map((user, index) => (
          <CreatorCard
            key={index}
            backGroundImage={user.backGroundImage}
            creatorProfileImage={user.creatorProfileImage}
            creatorName={user.creatorName}
            currentFollowers={user.currentFollowers}
          />
        ))}
      </div>
      <div className="flex justify-center mt-8 space-x-4">
        <Button 
            color="primary" 
            variant="ghost"
            radius="full"
            className='text-white'>
          Previous
        </Button>
        
        <Button 
            color="primary" 
            variant="ghost"
            radius="full"
            className='text-white'>
          Next
        </Button>
      </div>
    </div>

  )
}

export default Creators