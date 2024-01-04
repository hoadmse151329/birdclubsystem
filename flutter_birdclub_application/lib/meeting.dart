import 'dart:convert';

import 'package:flutter_birdclub_application/contest.dart';
import 'package:flutter_birdclub_application/fieldtrip.dart';
import 'package:http/http.dart' as http;
import './main.dart';
import 'package:flutter/material.dart';

class MeetingPage extends StatelessWidget {
  const MeetingPage({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        theme: ThemeData.dark().copyWith(
          scaffoldBackgroundColor: const Color.fromARGB(255, 18, 32, 47),
        ),
        home: MyHome());
  }
}

class MyHome extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final screenWidth = MediaQuery.of(context).size.width;
    final screenHeight = MediaQuery.of(context).size.height;
    return Scaffold(
      appBar: AppBar(
        title: Row(
          children: [
            CircleAvatar(
              backgroundColor: Colors.white,
              radius: screenWidth * 0.05,
              child: Padding(
                padding: EdgeInsets.all(screenWidth * 0.015),
                child: Image.asset(
                  'images/bird.jpg',
                  width: screenWidth * 0.1,
                  height: screenHeight * 0.1,
                ),
              ),
            ),
            SizedBox(width: screenWidth * 0.01),
            Text('Chao Mao Club'),
          ],
        ),
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            UserAccountsDrawerHeader(
              accountName: Text('Duy'),
              accountEmail: Text('hotrankhanhduy16@email.com'),
              currentAccountPicture: CircleAvatar(
                backgroundImage: AssetImage('images/avatar.jpg'),
              ),
            ),
            ListTile(
              leading: Icon(Icons.add_home),
              title: const Text(' Home '),
              onTap: () {
                Navigator.pop(context);
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => MyApp()),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.event),
              title: const Text(' Contest '),
              onTap: () {
                Navigator.pop(context);
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => ContestPage()),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.trip_origin),
              title: const Text(' Field Trip '),
              onTap: () {
                Navigator.pop(context);
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => FieldTripPage()),
                );
              },
            ),
            ListTile(
              leading: Icon(Icons.meeting_room),
              title: const Text(' Meeting '),
              onTap: () {
                Navigator.pop(context);
                Navigator.of(context).push(
                  MaterialPageRoute(builder: (context) => MeetingPage()),
                );
              },
            ),
          ],
        ),
      ),
      body: SingleChildScrollView(
        child: MeetingOnlineQrCode(),
      ),
    );
  }
}

class MeetingOnlineQrCode extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    final screenWidth = MediaQuery.of(context).size.width;
    final screenHeight = MediaQuery.of(context).size.height;
    return Container(
        width: screenWidth,
        height: screenHeight,
        decoration: BoxDecoration(color: Colors.white),
        child: Stack(children: [
          Positioned(
            left: 0,
            top: 0,
            child: Container(
              width: screenWidth,
              height: screenHeight * 0.55,
              child: Stack(
                children: [
                  Positioned(
                    left: 0,
                    top: 0,
                    child: Container(
                      width: screenWidth,
                      height: screenHeight * 0.3,
                      decoration: BoxDecoration(
                        image: DecorationImage(
                          image: AssetImage('images/Background.jpg'),
                          fit: BoxFit.fill,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
          Positioned(
            left: screenWidth * 0.05,
            top: screenHeight * 0.3,
            child: SizedBox(
              width: screenWidth * 0.9,
              height: screenHeight * 0.1,
              child: Text(
                'Scan to check in when meeting ends',
                textAlign: TextAlign.center,
                style: TextStyle(
                  color: Colors.black,
                  fontSize: screenWidth * 0.04,
                  fontWeight: FontWeight.w700,
                ),
              ),
            ),
          ),
          Positioned(
            left: screenWidth * 0.1,
            top: screenHeight * 0.4,
            child: Container(
              width: screenWidth * 0.8,
              height: screenHeight * 0.4,
              decoration: BoxDecoration(
                image: DecorationImage(
                  image: AssetImage('images/QR.jpg'),
                  fit: BoxFit.cover,
                ),
              ),
            ),
          ),
          Positioned(
            left: 0,
            top: screenHeight * 0.9, // Adjust the top position as needed
            child: Container(
              width: screenWidth,
              height: screenHeight, // Adjust the height as needed
              child: Stack(
                children: [
                  Positioned(
                    left: 0,
                    top: 0,
                    child: Container(
                      width: screenWidth,
                      height: screenHeight,
                      decoration: BoxDecoration(color: Color(0x591F5200)),
                    ),
                  ),
                  Positioned(
                    left: MediaQuery.of(context).size.width * 0.05,
                    top: MediaQuery.of(context).size.height * 0.05,
                    child: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.3,
                      height: MediaQuery.of(context).size.height * 0.06,
                      child: Text(
                        '@2024 ChaoMao BirdClub',
                        style: TextStyle(
                          color: Colors.black,
                          fontSize: MediaQuery.of(context).size.width * 0.028,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w400,
                          height: 0,
                          letterSpacing: 1.20,
                        ),
                      ),
                    ),
                  ),
                  Positioned(
                    left: MediaQuery.of(context).size.width * 0.15,
                    top: MediaQuery.of(context).size.height * 0.005,
                    child: SizedBox(
                      width: MediaQuery.of(context).size.width * 0.7,
                      height: MediaQuery.of(context).size.height * 0.1,
                      child: Text(
                        'We welcome donations in any amount to help fund club initiatives and activities.',
                        style: TextStyle(
                          color: Colors.black,
                          fontSize: MediaQuery.of(context).size.width * 0.03,
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w400,
                          height: 0,
                          letterSpacing: 1.20,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          )
        ]));
  }
}

class MeetingModel {
  final int id;
  final String meetingName;

  MeetingModel({required this.id, required this.meetingName});

  factory MeetingModel.fromJson(Map<String, dynamic> json) {
    return MeetingModel(
      id: json['id'],
      meetingName: json['meetingName'],
    );
  }
}
