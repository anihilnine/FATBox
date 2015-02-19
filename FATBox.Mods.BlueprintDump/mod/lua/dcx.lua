local GameMain = import('/lua/ui/game/gamemain.lua')

function _BeatFunction()
	import('/lua/instr.lua')
end

GameMain.AddBeatFunction(_BeatFunction)

LOG("===========test======")