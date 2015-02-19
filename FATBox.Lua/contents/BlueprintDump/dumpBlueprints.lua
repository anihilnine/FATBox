
function myLog(obj, indent)

    if indent == nil then indent = "" end

    if type(obj) == 'string' then
        local r = string.format("%q",obj)
        return r

    elseif type(obj) == 'userdata' then
        local r = tostring(obj)
        r = string.format("%q",r)
        return r

    elseif type(obj) == 'number' then
        if isINF(obj) then return nil end
        if isNAN(obj) then return nil end
        return tostring(obj)

    elseif type(obj) ~= 'table' then
        local r = tostring(obj)
        if string.find(r, 'INF') then
           r = r .. type(obj)
        end
        return r

    end


   local r
   local array =  isArray(obj)
   if isEmptyTable(obj) then return nil end
   if array then r = "[" else r = "{" end

   for k,v in obj do

      local newIndent = indent .. "  "
      local json = myLog(v, newIndent .. "  ")
      if json ~= nil then
  
        r = r .. "\n" .. newIndent
        if not array then
           r = r .. k .. " : " 
        end
        r = r .. json .. ","
      end

   end

   r = r .. "\n" .. indent

   if array then r = r .. "]" else r = r .. "}" end
  
   return r

end 

function isEmptyTable(t)
   for k,v in t do
       return false
   end
   return true
end 


function isArray(t)
   for k,v in t do
       return type(k) == 'number'
   end
end 

local inf = 1/0   
local ninf = 1/0   
function isINF(value)
  return value == inf or value == ninf
end

function isNAN(value)
  return value ~= value
end

LOG("<FATBox.BlueprintDump>")

local expected = 0
for k,v in __blueprints do
  if type(k) ~= 'number' then
	expected = expected + 1
  end
end

LOG("FATBox.BlueprintDump.Expected=" .. expected)


local current = 0
for k,v in __blueprints do
  local json = myLog(v)
  if type(k) ~= 'number' then -- blueprints get stored twice, as number and as string
	  if not string.find(json, "georock01_prop.bp") then -- cant remember ... i think this file is dodgy
	      LOG(json .. ',')
	  end
	  current = current + 1
	  LOG("FATBox.BlueprintDump.Current=" .. current)
	  if current == 30 then
		--break 
	  end
  end
end

LOG("</FATBox.BlueprintDump>")
