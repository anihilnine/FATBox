
local displayedTick = false
local lastUnitIds = {}
local brains = {}
local stps = 10

local lastReclaimableIds = {}

--function GoThread()
--	while true do
--		import('/lua/sinstr.lua')
--		WaitSeconds(1)
--
--		local a = new [0,0,0]
--		local b = new [100,100,100]
--		local c = new [0,0,0]
--		DrawLinePop(a,b,c)
--	end
--	
--end

function GoThread()

	local u = GetUnitById(0)
	LOG(repr(u));
	LOG(u:GetBlueprint().StrategicIconName)
	u:GetBlueprint().StrategicIconName = "icon_bot3_directfire"

	while true do
		import('/lua/sinstr.lua')
		WaitSeconds(1)

	end

end


function GoThread2()
	local wholeMap = Rect(0,0,10000,10000)
	while true do
		import('/lua/sinstr.lua')
		displayedTick = false
		showTick()

		local thisUnitIds = {}
		local thisReclaimableIds = {}

		#### units

		for ak, av in ListArmies() do

			local brain = brains[ak]
			if (brain == nil) then
				brain = GetArmyBrain(ak)
				brains[ak] = brain
				brain.dcx_props = {}
				brain.armyId = ak
			end

			local units = brain:GetListOfUnits(categories.ALLUNITS, false)
			for k,v in units do

				thisUnitIds[v:GetEntityId()] = true

				if lastUnitIds[v:GetEntityId()] == nil then
					dcxEntry("D", v:GetEntityId(), "Created2")
				end

				for ek, ev in v.dcx_events do
					dcxEntry("UE", v:GetEntityId(), ev)
					v.dcx_events[ek] = nil
				end

				local p =  v:GetPosition()
				compareUnit("UP", v, v:GetBlueprint().BlueprintId, "BlueprintId")
				compareUnit("UP", v, v:GetEntityId(), "Id")
				compareUnit("UP", v, v:GetArmy(), "Army")
				compareUnit("UP", v, math.floor(p.x), "X")
				compareUnit("UP", v, math.floor(p.y), "Y")
				compareUnit("UP", v, math.floor(p.z), "Z")
				compareUnit("UP", v, math.floor(v:GetMaxHealth()), "MaxHealth")
				compareUnit("UP", v, math.floor(v:GetHealth()), "Health")
				compareUnit("UP", v, v:GetBlueprint().StrategicIconName, "StrategicIconName")
				
			end

			

			compareArmy(brain, brain:GetEconomyIncome("MASS") * stps, "MassIncomePerSecond")
			compareArmy(brain, brain:GetEconomyIncome("ENERGY") * stps, "EnergyIncomePerSecond")

			compareArmy(brain, brain:GetArmyStat("Economy_Reclaimed_Mass", 0.0).Value, "TotalReclaim")
			compareArmy(brain, brain:GetArmyStat("Economy_income_reclaimed_Mass", 0.0).Value, "Economy_income_reclaimed_Mass")
			compareArmy(brain, brain:GetArmyStat("Economy_AccumExcess_Mass", 0.0).Value, "Economy_AccumExcess_Mass")
			compareArmy(brain, brain:GetArmyStat("Economy_Output_Mass", 0.0).Value, "Economy_Output_Mass")
			compareArmy(brain, brain:GetArmyStat("Economy_TotalConsumed_Mass", 0.0).Value, "Economy_TotalConsumed_Mass")
			compareArmy(brain, brain:GetArmyStat("Economy_Income_Mass", 0.0).Value, "Economy_Income_Mass")
			compareArmy(brain, brain:GetArmyStat("Economy_TotalProduced_Mass", 0.0).Value, "Economy_TotalProduced_Mass")

		end

		for k,v in lastUnitIds do
			if thisUnitIds[k] == nil then
				dcxEntry("UE", k, "Killed")
			end
		end
		lastUnitIds = thisUnitIds

		#### reclaim

		local reclaimables = GetReclaimablesInRect(wholeMap)
		for k, v in reclaimables do
			if v.MassReclaim ~= nil or v.EnergyReclaim ~= nil then

				thisReclaimableIds[v:GetEntityId()] = true
			
				if lastReclaimableIds[v:GetEntityId()] == nil then
					dcxEntry("RE", v:GetEntityId(), "Created2")
				end

				local p =  v.CachePosition
				compareUnit("RP", v, v:GetBlueprint().BlueprintId, "BlueprintId")
				compareUnit("RP", v, v:GetEntityId(), "Id")
				compareUnit("RP", v, math.floor(p.x), "X")
				compareUnit("RP", v, math.floor(p.y), "Y")
				compareUnit("RP", v, math.floor(p.z), "Z")
				compareUnit("RP", v, math.floor(v.EnergyReclaim), "EnergyReclaim")
				compareUnit("RP", v, math.floor(v.MassReclaim), "MassReclaim")
				compareUnit("RP", v, math.floor(v.MaxEnergyReclaim), "MaxEnergyReclaim")
				compareUnit("RP", v, math.floor(v.MaxMassReclaim), "MaxMassReclaim")
			end
		end
		
		for k,v in lastReclaimableIds do
			if thisReclaimableIds[k] == nil then
				dcxEntry("RE", k, "Killed")
			end
		end
		lastReclaimableIds = thisReclaimableIds

		#### end

		WaitSeconds(1)
	end 

end

function compareUnit(prefix, unit, val, name)
	local key = "dcx_last"..name
	if val ~= unit[key] then
		unit[key] = val
		dcxEntry(prefix, unit:GetEntityId(), name, val)
	end
end 

function compareArmy(armyBrain, val, name)
	local key = "dcx_last"..name
	if val ~= armyBrain[key] then
		armyBrain[key] = val
		dcxEntry("AP", armyBrain.armyId, name, val)
	end
end 

function showTick()
	local tick = GetGameTick()
	displayedTick = true
	dcxEntry("T", tick)
end

function dcxEntry(type, a, b, c)
	LOG(type .. "," .. (a or "") .. "," .. (b or "") .. "," .. (c or ""))	
end
