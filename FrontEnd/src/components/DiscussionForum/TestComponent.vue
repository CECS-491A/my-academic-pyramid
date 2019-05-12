<template>
<v-container>
  <v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

    <v-card-title>{{item.AccountName}}
    <v-spacer></v-spacer>
    {{item.DateCreated}}</v-card-title>


    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>
      <v-list-item class="grow">

        <v-list-item-content> {{ item.SchoolName + "/" + item.DepartmentName + "/" + item.CourseName }}     <v-spacer></v-spacer>

         {{"     Spam Count        : " + item.SpamCount + " Answers: " + item.AnswerCount + " Exp Needed to Answer: " + item.ExpNeededToAnswer}}
        <!-- </v-list-item-content>

        <v-list-item-content> -->
<v-spacer></v-spacer>

        <!-- <v-btn slot="activator" color="success">
        <v-icon left>mdi-bell</v-icon>
            Dropdown
        </v-btn>
        <v-card>
            <v-list dense>
            <v-list-tile
                v-for="option in options"
                :key="option"
            >
                <v-list-tile-title
                v-text="option"
                />
            </v-list-tile>
            </v-list>
        </v-card> -->



            <!-- <v-flex shrink pa-1> -->
                <!-- <v-select
                    v-model="options"
                    :items="options"
                    @input=""
                ></v-select> -->
            <!-- </v-flex> -->
        </v-list-item-content>    

        <v-layout align-center>
          <v-btn small>See Answers</v-btn>
          <v-btn small>Post Answer</v-btn>

<v-spacer></v-spacer>


          <v-btn small>See Answers</v-btn>
          <v-btn small>Post Answer</v-btn>

        </v-layout>
      </v-list-item>
    </v-card-actions>
  </v-card>
  </v-container>
</template>


<script>
import axios from 'axios'
  export default {
    name: "QuestionCard",
    data () {
      return {
        ismyQuestion: true,
        options: ["edit", "delete", "spam"],
        userAccount: null,
        isStudent: true,
        userId: "",
        school: "",
        schools: [],
        department: "",
        departments: [],
        course: "",
        courses: [],
        response: '',
        item: 
          {
            QuestionId: '11',
            SchoolName: 'CSULB',
            DepartmentName: 'Computer Science',
            CourseName: 'CECS491',
            AccountId: "10",
            AccountName: "account name ",
            Text: "a question ",
            ExpNeededToAnswer: "17",
            IsClosed: "false",
            SpamCount: "0",
            AnswerCount: "0",
            DateCreated: "05/12/19",
            DateUpdated: "05/15/19",
          }, 
        questions: [
        //   {
        //     QuestionId: '',
        //     SchoolName: '',
        //     DepartmentName: '',
        //     CourseName: '',
        //     AccountId: "",
        //     AccountName: "",
        //     Text: "",
        //     ExpNeededToAnswer: "",
        //     IsClosed: "",
        //     SpamCount: "",
        //     AnswerCount: "",
        //     DateCreated: "",
        //     DateUpdated: "",
        //   }, 
        ]
      }
    },

    created() {
        this.getSchoolQuestions()
    },
    
    // beforeMount(){
    //     this.userId = sessionStorage.SITuserId;
    //     //this.userId = "2"
    //     this.getAccount()
    // },
    methods: {
      getSchoolQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsBySchool",
              params: {
                  schoolId: 1
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
    }
  }
</script>

