using System;
using System.Collections.Generic;
using System.Linq;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.Game
{
    public class GameRecordKeeper
    {
        public IEnumerable<uint> RedTeamPlayerGameIds { get; private set; }
        public IEnumerable<uint> BlueTeamPlayerGameIds { get; private set; }
        public LogFileEnums.GameResult GameResult { get; private set; }
        public IEnumerable<RoundResults> RoundResults { get; private set; }

        public GameRecordKeeper(IEnumerable<RoundResults> results)
        {
            RoundResults = new List<RoundResults>(results);

            int redScore = 0;
            int blueScore = 0;
            int ties = 0;

            foreach (var round in RoundResults)
            {
                if (round.RoundResult == LogFileEnums.GameResult.BlueWin)
                {
                    blueScore++;
                }
                else if (round.RoundResult == LogFileEnums.GameResult.RedWin)
                {
                    redScore++;
                }
                else if (round.RoundResult == LogFileEnums.GameResult.TieGame)
                {
                    ties++;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            if (redScore > blueScore)
            {
                GameResult = LogFileEnums.GameResult.RedWin;
            }
            else if (redScore < blueScore)
            {
                GameResult = LogFileEnums.GameResult.BlueWin;
            }
            else
            {
                GameResult = LogFileEnums.GameResult.TieGame;
            }

            var r = RoundResults.SelectMany(s => s.RedRoundPlayerIds).Distinct().ToList();
            var b = RoundResults.SelectMany(s => s.BlueRoundPlayerIds).Distinct().ToList();

            RedTeamPlayerGameIds = new List<uint>(r);
            BlueTeamPlayerGameIds = new List<uint>(b);
        }
    }
}