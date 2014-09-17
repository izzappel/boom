angular.module('starter.controllers', [])

.controller('DashCtrl', function() {
})

.controller('FriendsCtrl', function($scope, Friends) {
  $scope.friends = Friends.all();
})

.controller('FriendDetailCtrl', function($scope, $stateParams, Friends) {
  $scope.friend = Friends.get($stateParams.friendId);
})

.controller('AccountCtrl', function() {
})

.controller('SurveysCtrl', function($scope, SurveyRest) {
        $scope.surveys = SurveyRest.allOpen();

})

.controller('SurveyDetailCtrl', function($scope, $stateParams, SurveyRest, ParticipantsRest) {
        $scope.survey = SurveyRest.get({ id: $stateParams.surveyId });

        $scope.participant = {Id: undefined};
        $scope.participate = function (participant) {
            $scope.participant = participant;
            ParticipantsRest.create({
                surveyId: $scope.survey.Id,
                participant: $scope.participant
            });
        };

        $scope.hasJoined = function(){
            return $scope.participant.Id !== undefined;
        };

        $scope.hasOptionsSelected = function(){
            var optSelected = false;
            angular.forEach($scope.survey.Options, function(option){
                if(option.selected){
                    optSelected = true;
                }
            });
            return optSelected;
        };
});
