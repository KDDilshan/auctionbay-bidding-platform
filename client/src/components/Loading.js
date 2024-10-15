import { Spinner } from '@nextui-org/react';
import React from 'react'

function Loading() {
  return (
    <div className="w-full h-screen flex justify-center items-center">
      <Spinner size="md" label="Loading..." />
    </div>
  );
}

export default Loading
