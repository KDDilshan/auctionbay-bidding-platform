import React from 'react'
import { PiDiscordLogoLight,
         PiYoutubeLogoLight,
         PiTwitterLogoLight,
         PiInstagramLogoLight } from "react-icons/pi";
import {Input} from "@nextui-org/react";
import {Button} from "@nextui-org/react";


function Footer() {
  return (
    <footer className="py-12 pb-16 text-white bg-gray-800 bg-opacity-40 m-28 border-1 rounded-2xl">
        <div className="container grid grid-cols-1 px-32 mx-auto -mb-4 gap-28 md:grid-cols-3">
            <div className=''>
                <h3 className="mb-4 text-lg font-semibold">NFTKING</h3>
                <p className="mb-4 text-sm font-thin">NFT marketplace UI created with Anima for Figma.</p>
                <h3 className="mt-4 font-thin text-white">Join our community</h3>
                <div className="flex space-x-4 text-3xl ">
                    
                    <PiDiscordLogoLight />
                    <PiYoutubeLogoLight />
                    <PiTwitterLogoLight/>
                    <PiInstagramLogoLight />
            
                </div>
            </div>

            <div className='ml-6'>
                <h3 className="mb-4 text-lg font-semibold">Explore</h3>
                <ul className="space-y-2 font-thin">
                    <li>
                        <a href="#" className="hover:text-blue-600">Marketplace</a>
                    </li>
                    <li>
                        <a href="#" className="hover:text-blue-600">Ranking</a>
                    </li>
                    <li>
                        <a href="#" className="hover:text-blue-600">Connect a Wallet</a>
                    </li>
                </ul>
            </div>

            <div>
                <h3 className="mb-6 text-lg font-semibold">Join our weekly digest</h3>
                <p className="mb-4 text-sm font-thin">Get exclusive promotions & updates straight to your inbox.</p>
                <div className="flex flex-row w-full gap-2 ">
                    <Input
                        radius='full'
                        type="email"
                        placeholder="Enter your email address"
                        className="max-w-[220px]"
                    />
                    <Button
                        radius="full"
                        size="md"
                        color="primary"
                        className="px-10 text-white bg-gradient-to-tr from-blue-400 to-blue-900"> 
                        Subscribe 
                    </Button>
                </div>
            </div>
        </div>

        <hr className="mx-32 my-8 border-gray-600" />
        <p className="mb-4 ml-32 -mt-4 text-sm font-thin text-left">
        <span className='text-lg font-thin'>&copy; </span>NFT Market. Use this template freely.</p>
    </footer>
  )
}

export default Footer