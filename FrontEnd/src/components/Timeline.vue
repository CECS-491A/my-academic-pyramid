<template>

  <v-app id="timeline">
    <v-timeline>
      <v-timeline-item
        color="purple lighten-2"
        fill-dot
        right
      >
        <v-card>
          <v-card-title class="purple lighten-2">
            <v-icon
              dark
              size="42"
              class="mr-3"
            >
              mdi-magnify
            </v-icon>
            <h2 class="display-1 white--text font-weight-light">Project was started</h2>
          </v-card-title>
          <v-container>
            <v-layout>
              <v-flex xs10>
                In Fall 2018 , we start to design the My Academic Pyramid application. Our team has 6 members . Our professor is Vatanak Vong
              </v-flex>
              <v-flex xs2>
                <v-icon size="64">mdi-calendar-text</v-icon>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
  
      <v-timeline-item
        color="amber lighten-1"
        fill-dot
        left
        small
      >
        <v-card>
          <v-card-title class="amber lighten-1 justify-end">
            <h2 class="display-1 mr-3 white--text font-weight-light">Design phase was finished</h2>
            <v-icon
              dark
              size="42"
            >mdi-home-outline</v-icon>
          </v-card-title>
          <v-container>
            <v-layout>
              <v-flex xs8>
                At the end of Fall 2018 semester, we finished all design document and got ready to go to implemetation phase
              </v-flex>

            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
  
      <v-timeline-item
        color="cyan lighten-1"
        fill-dot
        right
      >
        <v-card>
          <v-card-title class="cyan lighten-1">
            <v-icon
              class="mr-3"
              dark
              size="42"
            >
              mdi-email-outline
            </v-icon>
            <h2 class="display-1 white--text font-weight-light">Start implementation</h2>
          </v-card-title>
          <v-container>
            <v-layout>
              <v-flex

              >
                At the beginning of Spring 2019, we started implementaion based on the design last semester
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
  
      <v-timeline-item
        color="red lighten-1"
        fill-dot
        left
        small
      >
        <v-card>
          <v-card-title class="red lighten-1 justify-end">
            <h2 class="display-1 mr-3 white--text font-weight-light">Project is finished</h2>
            <v-icon
              dark
              size="42"
            >
              mdi-account-multiple-outline
            </v-icon>
          </v-card-title>
          <v-container>
            <v-layout>
              <v-flex xs2>
                <v-icon size="64">mdi-server-network</v-icon>
              </v-flex>
              <v-flex xs10>
                Our project is finished. Get ready for the presentation
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
  
      <v-timeline-item
        color="green lighten-1"
        fill-dot
        right
      >
        <v-card>
          <v-card-title class="green lighten-1">
            <v-icon
              class="mr-3"
              dark
              size="42"
            >
              mdi-phone-in-talk
            </v-icon>
            <h2 class="display-1 white--text font-weight-light">Presentation day</h2>
          </v-card-title>
          <v-container>
            <v-layout>
              <v-flex>
                Coming soon
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-timeline-item>
    </v-timeline>
  </v-app>
</template>
