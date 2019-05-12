<template>
<v-app>
  <v-layout row>
    <v-flex xs12 sm6 offset-sm3>
      <v-card dark>
        <v-toolbar color="blue" dark>
          <v-toolbar-title centered>Discussion Forum</v-toolbar-title>  

          <v-spacer></v-spacer>

          <v-menu :nudge-width="50">
            <template v-slot:activator="{ on }">
              <v-toolbar-title v-on="on">
                <span>School</span>
                <v-icon dark>arrow_drop_down</v-icon>
              </v-toolbar-title>
            </template>

            <v-list>
              <v-list-tile
                v-for="item in schools"
                :key="item"
                @click=""
              >
                <v-list-tile-title v-text="item"></v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>

          <v-menu :nudge-width="100">
            <template v-slot:activator="{ on }">
              <v-toolbar-title v-on="on">
                <span>Department</span>
                <v-icon dark>arrow_drop_down</v-icon>
              </v-toolbar-title>
            </template>

            <v-list>
              <v-list-tile
                v-for="item in departments"
                :key="item"
                @click=""
              >
                <v-list-tile-title v-text="item"></v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>

          <v-menu :nudge-width="100">
            <template v-slot:activator="{ on }">
              <v-toolbar-title v-on="on">
                <span>Course</span>
                <v-icon dark>arrow_drop_down</v-icon>
              </v-toolbar-title>
            </template>

            <v-list>
              <v-list-tile
                v-for="item in courses"
                :key="item"
                @click=""
              >
                <v-list-tile-title v-text="item"></v-list-tile-title>
              </v-list-tile>
            </v-list>
          </v-menu>
          
          <v-btn small> Post </v-btn>

          <v-btn icon>
            <v-icon>refresh</v-icon>
          </v-btn>
        </v-toolbar>



        <v-toolbar color="pink" dark>
          <v-toolbar-side-icon class="grey--text" @click="drawer =!drawer"></v-toolbar-side-icon>

          <v-toolbar-title>Questions</v-toolbar-title>

          <v-spacer></v-spacer>

          

          <!-- <v-btn icon>
            <v-icon>search</v-icon>
          </v-btn>

          <v-btn icon>
            <v-icon>check_circle</v-icon>
          </v-btn> -->
        </v-toolbar>

        

        <v-list three-line>

            <v-navigation-drawer disable-resize-warcher app v-model="drawer" class="info">
          <v-layout column align-center>
            <v-flex class="mt-5">
          <p class="purple--text subheading mt-1">User Name</p>
        </v-flex>
      </v-layout>
      <v-list>
        <v-list-tile v-for="link in links" :key="link.name" router :to="link.route">
          <v-list-tile-action>
            <v-icon class="purple--text">{{link.icon}}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title class="purple--text">{{link.text}}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
          <!-- <template v-for="(item, index) in questions"> -->
            <template v-for="item in questions">
            <v-list-tile
              :key="item.Id"
              avatar
              ripple
              star_border
              @click=""
            >
              <v-list-tile-content>
                <v-list-tile-title>{{ item.AccountName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">{{ item.Text }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.SchoolName + "/" + item.DepartmentName + "/" + item.CourseName 
                    + " Spam Count: " + item.SpamCount + " Answers: " + item.AnswerCount + " Exp Needed to Answer: " + item.ExpNeededToAnswer}}</v-list-tile-sub-title>
              </v-list-tile-content>

              <v-list-tile-action>
                <v-list-tile-action-text>{{ item.DateCreated }}</v-list-tile-action-text>
                
                <div>
                  <v-btn flat icon color="blue lighten-2">
                    <v-icon>thumb_up</v-icon>
                  </v-btn>

                  <v-btn flat icon color="red lighten-2">
                    <v-icon>thumb_down</v-icon>
                    </v-btn>
                </div>

                <div>
                  <v-btn small>Answers</v-btn>
                  <v-btn small>Answer</v-btn>
                </div>

                <!-- <v-icon
                  v-if="selected.indexOf(index) < 0"
                  color="grey lighten-1"
                >
                  star_border
                </v-icon>

                <v-icon
                  v-else
                  color="yellow darken-2"
                >
                  star
                </v-icon> -->
              </v-list-tile-action>

            </v-list-tile>
            <v-divider
              v-if="index + 1 < questions.length"
              :key="index"
            ></v-divider>
          </template>
        </v-list>
      </v-card>
    </v-flex>
  </v-layout>
</v-app>
</template>

<script>
import axios from 'axios'

  export default {
    data () {
      return {
        //selected: [2],
        selected: 'A',
            options: [
                { text: 'One', value: 'A' },
                { text: 'Two', value: 'B' },
                { text: 'Three', value: 'C' }
            ],
            questions: [],
            schools: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            departments: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            courses: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            drawer: false,
            links: [
                { name: 'Questions', text: 'Questions', route: '/DiscussionForum'},
                { name: 'Drafts', text: 'Drafts', route: '/DiscussionForum'}
            ],
            response: ''
        // questions: [
        //   {
        //     questionId: '',
        //     schoolName: '',
        //     departmentName: '',
        //     courseName: '',
        //     accountId: "",
        //     accountName: "",
        //     text: "",
        //     expNeededToAnswer: "",
        //     isClosed: "",
        //     spamCount: "",
        //     answerCount: "",
        //     dateCreated: "",
        //     dateUpdated: "",
        //   }
        //]
      }
    },
    created() {
        this.getSchoolQuestions()
    },
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
        // toggle (index) {
        //   const i = this.selected.indexOf(index)
        //   if (i > -1) {
        //     this.selected.splice(i, 1)
        //   } else {
        //     this.selected.push(index)
        //   }
        // }
    }
  }
</script>