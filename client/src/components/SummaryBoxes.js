import React from "react";

function SummaryBoxes({ list }) {
  return (
    <div className="w-full flex gap-5">
      {list.map((item, index) => (
        <div className="flex items-center gap-4 py-5 px-5 w-1/4 bg-zinc-900 rounded-xl">
          <div className="p-3 bg-zinc-700 rounded-full text-3xl">
            {item.icon}
          </div>
          <div>
            <div className="text-xl">{item.name}</div>
            <div className="text-2xl font-mono">{item.detail}</div>
          </div>
        </div>
      ))}
    </div>
  );
}

export default SummaryBoxes;
