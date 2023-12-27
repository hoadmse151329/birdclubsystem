import './main.dart';
import 'package:flutter/material.dart';

class FieldTripPage extends StatelessWidget {
  const FieldTripPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: const MyApp(),
      appBar: AppBar(title: const Text("About")),
      body: const Center(child: Text("About")),
    );
  }
}
