using System;

namespace Api
{
    [Serializable]
    public class RecordResultDto
    {
        private int user_id;
        
        private bool animalKill;

        private bool pedestrianKill;

        private bool carAccident;

        private bool illegalLaneChange;

        private bool signalViolatio;

        private bool centerLineViolation;

        private bool offPath;

        private bool maxSpeed;

        private bool wiperCount;

        private bool klaxonCount;

        private bool clearCount;

        private bool minClearTime;

        private bool achievementCount;

        public RecordResultDto(int userID, bool animalKill, bool pedestrianKill, bool carAccident, bool illegalLaneChange, bool signalViolatio, bool centerLineViolation, bool offPath, bool maxSpeed, bool wiperCount, bool klaxonCount, bool clearCount, bool minClearTime, bool achievementCount)
        {
            user_id = userID;
            this.animalKill = animalKill;
            this.pedestrianKill = pedestrianKill;
            this.carAccident = carAccident;
            this.illegalLaneChange = illegalLaneChange;
            this.signalViolatio = signalViolatio;
            this.centerLineViolation = centerLineViolation;
            this.offPath = offPath;
            this.maxSpeed = maxSpeed;
            this.wiperCount = wiperCount;
            this.klaxonCount = klaxonCount;
            this.clearCount = clearCount;
            this.minClearTime = minClearTime;
            this.achievementCount = achievementCount;
        }
    }
}